namespace AmplaEvents.RecordChanged
{
    public interface IModuleRecord
    {
        /// <summary>
        /// Gets the record Id.
        /// </summary>
        int RecordId { get; }

        /// <summary>
        /// The Location of the record
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Gets the module.
        /// </summary>
        string Module { get; }
    }
}