using System;

public readonly struct InventoryItemId : IEquatable<InventoryItemId>
{
    public Guid Value { get; }

    public InventoryItemId(Guid value)
    {
        Value = value;
    }

    public static InventoryItemId New()
       => new(Guid.NewGuid());

    public bool Equals(InventoryItemId other)
       => Value.Equals(other.Value);

    public override bool Equals(object obj)
       => obj is InventoryItemId other && Equals(other);

    public override int GetHashCode()
       => Value.GetHashCode();

    public static bool operator ==(InventoryItemId a, InventoryItemId b) => a.Equals(b);
    public static bool operator !=(InventoryItemId a, InventoryItemId b) => !a.Equals(b);

    public override string ToString()
       => Value.ToString();
}
