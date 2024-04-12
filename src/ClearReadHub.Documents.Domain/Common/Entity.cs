namespace ClearReadHub.Documents.Domain.Common;

/**
 * Base class for all entities in the domain.
 * <summary>
 * An entity is an object that has an identity and is defined by its attributes.
 * </summary>
 */
public abstract partial class Entity
{
    /**
     * The unique identifier for this entity.
     * <summary>
     * Gets and sets the Id. This is set when the entity is created and cannot be changed.
     * </summary>
     */
    public Guid Id { get; private init; }
    /**
     * The time this entity was created.
     * <summary>
     *  Gets the Created DateTime of the entity.
     *  This is set to the current time when the entity is created.
     * </summary>
     */
    public DateTime CreatedAt { get; }
    /**
     * The last time this entity was updated.
     * <summary>
     * Gets the Updated DateTime of the entity.
     * This is set to the current time when the entity is created and updated.
     * </summary>
     */
    public DateTime UpdatedAt { get; }
}

/**
 * Base class for all entities in the domain.
 * <summary>
 * An entity is an object that has an identity and is defined by its attributes.
 * </summary>
 */
public abstract partial class Entity
{
    private readonly List<IDomainEvent> domainEvents = [];

    /**
     * Constructor for the entity.
     * <summary>
     * Initializes a new instance of the <see cref="Entity"/> class.
     * </summary>
     */
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /**
    * Constructor for the entity.
    * <summary>
    * Initializes a new instance of the <see cref="Entity"/> class.
    * </summary>
    * <param name="id">The unique identifier for this entity.</param>
    */
    protected Entity(Guid id)
    {
      this.Id = id;
      this.CreatedAt = DateTime.UtcNow;
      this.UpdatedAt = DateTime.UtcNow;
     }

    /**
    * Adds a domain event to the entity.
    * <summary>
    * Adds a domain event to the entity.
    * </summary>
    * <param name="event">The domain event to add.</param>
    * <returns>The entity with the domain event added.</returns>
    */
    public List<IDomainEvent> PopDomainEvents()
    {
      var copy = domainEvents.ToList();
      domainEvents.Clear();

      return copy;
    }
}