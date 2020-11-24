namespace Common.Contracts
{
    /// <summary>
    /// This enumerator contains the vehicle different types.
    /// </summary>
    public enum VehicleType
    {
        Car = 0,
        MotorCycle = 1,
        Truck = 2,
        Van = 3,
        Other = 4
    }

    /// <summary>
    /// This enumerator contains the reparation status different types.
    /// </summary>
    public enum ReparationStatus
    {
        Pending = 0,
        Repairing = 1,
        Repaired =2,
        Rejected = 3
    }

    /// <summary>
    /// This enumerator contains the reparation different types.
    /// </summary>
    public enum ReparationType
    {
        Maintenance = 0,
        Reparation = 1,
        Other = 2
    }
}