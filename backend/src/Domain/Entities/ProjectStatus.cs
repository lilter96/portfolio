namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Integrity label for every portfolio project.
    /// Maps to the content integrity rules: every claim must link to evidence.
    /// </summary>
    public enum ProjectStatus
    {
        /// <summary>Deployed and clickable (e.g., TGSlots at railway.app).</summary>
        Live = 0,

        /// <summary>Real but not shipped — specs, MVPs, WIP.</summary>
        Personal = 1,

        /// <summary>
        /// Production work that cannot be linked due to NDA.
        /// Described by architecture, scope, and role — never with invented metrics or client names.
        /// </summary>
        WorkNda = 2,

        /// <summary>Public repository, linked.</summary>
        OpenSource = 3
    }
}
