using Nop.Data;
using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Services;
public class ResponseService : IResponseService
{
    private readonly IRepository<Responses> _responsesRepository;

    public ResponseService(
        IRepository<Domain.Responses> responsesRepository
        )
    {
        _responsesRepository = responsesRepository;
    }

    public async Task InsertResponseAsync(Responses response)
    {
        await _responsesRepository.InsertAsync(response);
    }
}
