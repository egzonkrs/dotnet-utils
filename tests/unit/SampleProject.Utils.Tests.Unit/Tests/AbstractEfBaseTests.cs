using Xunit;
using System;
using System.Linq;
using System.Transactions;
using System.Threading.Tasks;
using SampleProject.Utils.Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Utils.Data.EntityFrameworkCore.Extensions;
using SampleProject.Utils.Tests.Unit.Mocks;
using SampleProject.Utils.Tests.Unit.Specifications;

namespace SampleProject.Utils.Tests.Unit.Tests;

public abstract class AbstractEfRepositoryTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IUnitOfWork<MockDbContext> _unitOfWork;
    private readonly IRepository<MockEntity> _repository;

    protected AbstractEfRepositoryTests(Action<IServiceCollection> configureServices)
    {
        var services = new ServiceCollection();
        configureServices(services);
        services.AddEfCoreRepository<MockEntity, MockRepository>();
        services.AddEfCoreUnitOfWork<MockDbContext>();

        _serviceProvider = services.BuildServiceProvider();
        _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork<MockDbContext>>();
        _repository = _serviceProvider.GetRequiredService<IRepository<MockEntity>>();

        var dbContext = _serviceProvider.GetRequiredService<MockDbContext>();
        dbContext.Database.EnsureCreated();
    }

    public void Dispose() => _serviceProvider.Dispose();

    [Fact]
    public async Task InsertAsync_WithSaveChanges_ReturnsInsertedDataWithCorrectPrimaryKey()
    {
        // Arrange
        const string tenant = "myTenant";
        var entity = new MockEntity { TenantId = tenant };

        // Act
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var result = await _repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tenant, result.TenantId);
    }

    [Fact]
    public async Task UpdateAsync_WithSaveChangesAndComplete_ReturnsUpdatedData()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        var entity = new MockEntity { TenantId = tenant };
        MockEntity result = null;
        await _unitOfWork.ExecuteTransactionAsync(async x =>
        {
            await _repository.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            result = await _repository.GetByIdAsync(entity.Id);
            x.Complete();
        }, IsolationLevel.RepeatableRead);

        // Act
        result.TenantId = otherTenant;
        await _repository.UpdateAsync(result);
        await _unitOfWork.SaveChangesAsync();
        result = await _repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(otherTenant, result.TenantId);
    }

    [Fact]
    public async Task GetAllAsync_WithNoSpecification_ReturnsAllData()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = otherTenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(tenant, result.FirstOrDefault().TenantId);
        Assert.Equal(otherTenant, result.LastOrDefault().TenantId);
    }

    [Fact]
    public async Task GetAllAsync_WithSpecification_ReturnsAllFilteredData()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = otherTenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync(new TenantSpecification(tenant));

        // Assert
        Assert.Single(result);
        Assert.Equal(tenant, result.FirstOrDefault().TenantId);
    }

    [Fact]
    public async Task GetAllAsync_WithTenantsQueryFilter_ReturnsAllFilteredData()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        // _userContextAccessor.SetTenants(tenant);
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = otherTenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(tenant, result.FirstOrDefault().TenantId);
    }

    [Fact]
    public async Task GetAllAsync_WithTenantsQueryFilterAndSpecification_ReturnsEmpty()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        // _userContextAccessor.SetTenants(tenant);
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = otherTenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync(new TenantSpecification(otherTenant));

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithPrimaryId_ReturnsCorrectEntity()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(entity);
        await _repository.InsertAsync(new MockEntity { TenantId = otherTenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(tenant, result.TenantId);
    }

    [Fact]
    public async Task ExistsAsync_WithValidPrimaryId_ReturnsTrue()
    {
        // Arrange
        const string tenant = "myTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync(entity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidPrimaryId_ReturnsFalse()
    {
        // Arrange
        const string tenant = "myTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_WithValidSpecification_ReturnsTrue()
    {
        // Arrange
        const string tenant = "myTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync(new TenantSpecification(tenant));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNoSpecification_ReturnsTrue()
    {
        // Arrange
        const string tenant = "myTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidSpecification_ReturnsFalse()
    {
        // Arrange
        const string tenant = "myTenant";
        const string otherTenant = "otherTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync(new TenantSpecification(otherTenant));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllAsync_WithPaging_ReturnsAllPage()
    {
        // Arrange
        const string tenant = "tenant1";
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync(new TenantSpecification(tenant) { PageNumber = 1, PageSize = 5 });

        // Assert
        Assert.Equal(4, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_WithPagingInvalidPage_ReturnsEmpty()
    {
        // Arrange
        const string tenant = "tenant1";
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _repository.InsertAsync(new MockEntity { TenantId = tenant });
        await _unitOfWork.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync(new TenantSpecification(tenant) { PageNumber = 2, PageSize = 5 });

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task DeleteAsync_WithEntityFromGetById_DeletesCorrectEntity()
    {
        // Arrange
        const string tenant = "tenant1";
        const string otherTenant = "otherTenant";
        var entity = new MockEntity { TenantId = tenant };
        await _repository.InsertAsync(new MockEntity { TenantId = otherTenant });
        await _repository.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var entityToDelete = await _repository.GetByIdAsync(entity.Id);

        // Act
        await _repository.DeleteAsync(entityToDelete);
        await _unitOfWork.SaveChangesAsync();
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(otherTenant, result.FirstOrDefault().TenantId);
    }
}