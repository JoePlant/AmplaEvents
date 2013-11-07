namespace AmplaEvents.RecordChanged
{
    /// <summary>
    ///     Base class for Ampla record messages.
    /// </summary>
    public abstract class ModuleRecord : IModuleRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleRecord"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="module">The module.</param>
        /// <param name="recordId">The record unique identifier.</param>
        protected ModuleRecord(string location, string module, int recordId)
        {
            Location = location;
            Module = module;
            RecordId = recordId;
        }

        /// <summary>
        /// Gets the record Id.
        /// </summary>
        public int RecordId { get; private set; }

        /// <summary>
        /// Gets the location of the record
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// Gets the module.
        /// </summary>
        public string Module { get; private set; }
    }
}