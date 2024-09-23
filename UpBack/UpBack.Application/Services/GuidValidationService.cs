using UpBack.Application.Exceptions;

namespace UpBack.Application.Services
{
    public class GuidValidationService : IGuidValidationService
    {
        public Guid ValidateGuid(string id)
        {
            if (!Guid.TryParse(id, out var validGuid))
            {
                throw new InvalidGuidException("The ID must be a valid GUID.");
            }

            return validGuid;
        }
    }
}
