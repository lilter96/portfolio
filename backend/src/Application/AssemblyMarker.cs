namespace Portfolio.Application;

/// <summary>
/// Marker type for assembly scanning.
/// </summary>
public static class AssemblyMarker
{
    /// <summary>
    /// Returns the assembly-qualified name for this marker.
    /// </summary>
    public static string Reference => typeof(AssemblyMarker).Assembly.FullName!;
}
