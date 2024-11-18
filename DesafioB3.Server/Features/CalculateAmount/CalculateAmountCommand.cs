using DesafioB3.Core.Domain;
using MediatR;

namespace DesafioB3.API.Features.CalculateAmount;

public record CalculateAmountCommand(decimal InitialValue, int Quantity) : IRequest<CalculateAmountResponse>, ICommand;