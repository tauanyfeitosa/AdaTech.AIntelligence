using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.WebAPI")]
namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ErrosCustomer
{
    /// <summary>
    /// Represents details of an error.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Gets or sets the status code of the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message of the error.
        /// </summary>
        public string Message { get; set; }
    }
}
