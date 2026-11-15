using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Services.Contract;
using SistemadeMensajeria.Services.Dtos;
using SistemadeMensajeria.Services.Core;
namespace SistemadeMensajeria.Services.Services;
public class EnvioService(IRepositorio<Envio> repo, IRepositorio<Cliente> clientes, IRepositorio<Mensajero> mensajeros, IRepositorio<Direccion> direcciones, IUnitOfWork unitOfWork) : BaseService, IEnvioService {
 public async Task<IReadOnlyList<EnvioDto>> ObtenerTodosAsync(CancellationToken ct=default)=>(await repo.ObtenerTodosAsync(ct)).Select(Map).ToList();
 public async Task<EnvioDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); return x is null?null:Map(x);}
 public async Task<EnvioDto> CrearAsync(EnvioRequestDto dto,CancellationToken ct=default) { if(await clientes.ObtenerPorIdAsync(dto.ClienteId,ct) is null || await mensajeros.ObtenerPorIdAsync(dto.MensajeroId,ct) is null || await direcciones.ObtenerPorIdAsync(dto.DireccionOrigenId,ct) is null || await direcciones.ObtenerPorIdAsync(dto.DireccionDestinoId,ct) is null) throw new ArgumentException("Uno o más identificadores relacionados no existen."); var x=await repo.AgregarAsync(new Envio { ClienteId=dto.ClienteId, MensajeroId=dto.MensajeroId, DireccionOrigenId=dto.DireccionOrigenId, DireccionDestinoId=dto.DireccionDestinoId, Descripcion=dto.Descripcion.Trim(), Costo=dto.Costo, Estado=dto.Estado.Trim() },ct); return Map(x); }
 public async Task<bool> ActualizarAsync(int id,EnvioRequestDto dto,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); if(x is null)return false; if(await clientes.ObtenerPorIdAsync(dto.ClienteId,ct) is null || await mensajeros.ObtenerPorIdAsync(dto.MensajeroId,ct) is null || await direcciones.ObtenerPorIdAsync(dto.DireccionOrigenId,ct) is null || await direcciones.ObtenerPorIdAsync(dto.DireccionDestinoId,ct) is null) throw new ArgumentException("Uno o más identificadores relacionados no existen."); x.ClienteId=dto.ClienteId; x.MensajeroId=dto.MensajeroId; x.DireccionOrigenId=dto.DireccionOrigenId; x.DireccionDestinoId=dto.DireccionDestinoId; x.Descripcion=dto.Descripcion.Trim(); x.Costo=dto.Costo; x.Estado=dto.Estado.Trim(); x.FechaEntrega=dto.FechaEntrega; await repo.ActualizarAsync(x,ct); return true;}
 public Task<bool> EliminarAsync(int id,CancellationToken ct=default)=>repo.EliminarAsync(id,ct);
 private static EnvioDto Map(Envio x)=>new(x.EnvioId,x.ClienteId,x.MensajeroId,x.DireccionOrigenId,x.DireccionDestinoId,x.Descripcion,x.Costo,x.Estado,x.FechaCreacion,x.FechaEntrega);
}
