
namespace AdaTech.AIntelligence.Attributes
{
    /// <summary>
    /// Specifies a display name for Swagger documentation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SwaggerDisplayNameAttribute : Attribute
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerDisplayNameAttribute"/> class with the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        public SwaggerDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

}
