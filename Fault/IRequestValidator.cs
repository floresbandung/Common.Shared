using System.Threading.Tasks;
using DD.Tata.Buku.Shared.Fault;

namespace DD.TataBuku.Shared.Fault
{
    public interface IRequestValidator<in TRequest>
    {
        Task<ValidationResult> Validate(TRequest request);
        int Order { get; }
    }
}
