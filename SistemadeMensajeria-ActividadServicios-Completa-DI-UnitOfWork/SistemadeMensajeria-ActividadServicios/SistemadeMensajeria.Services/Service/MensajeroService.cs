using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Services.Contract;
using SistemadeMensajeria.Services.Dtos;
using SistemadeMensajeria.Services.Core;
namespace SistemadeMensajeria.Services.Services;
public class MensajeroService(IRepositorio<Mensajero> repo, IUnitOfWork unitOfWork) : BaseService, IMensajeroService {
 public async Task<IReadOnlyList<MensajeroDto>> ObtenerTodosAsync(CancellationToken ct=default)=>(await repo.ObtenerTodosAsync(ct)).Select(Map).ToList();
 public async Task<MensajeroDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); return x is null?null:Map(x);}
 public async Task<MensajeroDto> CrearAsync(MensajeroRequestDto dto,CancellationToken ct=default) {  var x=await repo.AgregarAsync(new Mensajero { Nombres=dto.Nombres.Trim(), Apellidos=dto.Apellidos.Trim(), Telefono=dto.Telefono.Trim(), Identificacion=dto.Identificacion.Trim(), Disponible=dto.Disponible },ct); return Map(x); }
 public async Task<bool> ActualizarAsync(int id,MensajeroRequestDto dto,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); if(x is null)return false;  x.Nombres=dto.Nombres.Trim(); x.Apellidos=dto.Apellidos.Trim(); x.Telefono=dto.Telefono.Trim(); x.Identificacion=dto.Identificacion.Trim(); x.Disponible=dto.Disponible; await repo.ActualizarAsync(x,ct); return true;}
 public Task<bool> EliminarAsync(int id,CancellationToken ct=default)=>repo.EliminarAsync(id,ct);
 private static MensajeroDto Map(Mensajero x)=>new(x.MensajeroId,x.Nombres,x.Apellidos,x.Telefono,x.Identificacion,x.Disponible);
}
