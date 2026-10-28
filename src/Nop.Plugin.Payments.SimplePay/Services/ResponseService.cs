using Nop.Data;
using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Services;
public class ResponseService : IResponseService
{
    private readonly IRepository<ResponseEvents> _responseEventsRepository;
    private readonly IRepository<Responses> _responsesRepository;

    public ResponseService(
        IRepository<ResponseEvents> responseEventsRepository,
        IRepository<Responses> responsesRepository
        )
    {
        _responseEventsRepository = responseEventsRepository;
        _responsesRepository = responsesRepository;
    }

    public async Task InsertResponseAsync(Responses response)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(response.Events);

        await _responseEventsRepository.InsertAsync(response.Events);
        response.EventId = response.Events.Id;
        await _responsesRepository.InsertAsync(response);
    }
}
