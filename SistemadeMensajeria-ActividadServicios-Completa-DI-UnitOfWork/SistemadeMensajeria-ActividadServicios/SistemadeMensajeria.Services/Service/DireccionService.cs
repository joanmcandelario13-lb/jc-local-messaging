using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Services.Contract;
using SistemadeMensajeria.Services.Dtos;
using SistemadeMensajeria.Services.Core;
namespace SistemadeMensajeria.Services.Services;
public class DireccionService(IRepositorio<Direccion> repo, IRepositorio<Cliente> clientes, IUnitOfWork unitOfWork) : BaseService, IDireccionService {
 public async Task<IReadOnlyList<DireccionDto>> ObtenerTodosAsync(CancellationToken ct=default)=>(await repo.ObtenerTodosAsync(ct)).Select(Map).ToList();
 public async Task<DireccionDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); return x is null?null:Map(x);}
 public async Task<DireccionDto> CrearAsync(DireccionRequestDto dto,CancellationToken ct=default) { if(await clientes.ObtenerPorIdAsync(dto.ClienteId,ct) is null) throw new ArgumentException("El cliente indicado no existe."); var x=await repo.AgregarAsync(new Direccion { ClienteId=dto.ClienteId, Calle=dto.Calle.Trim(), Numero=dto.Numero.Trim(), Sector=dto.Sector.Trim(), Ciudad=dto.Ciudad.Trim(), Referencia=dto.Referencia.Trim() },ct); return Map(x); }
 public async Task<bool> ActualizarAsync(int id,DireccionRequestDto dto,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); if(x is null)return false; if(await clientes.ObtenerPorIdAsync(dto.ClienteId,ct) is null) throw new ArgumentException("El cliente indicado no existe."); x.ClienteId=dto.ClienteId; x.Calle=dto.Calle.Trim(); x.Numero=dto.Numero.Trim(); x.Sector=dto.Sector.Trim(); x.Ciudad=dto.Ciudad.Trim(); x.Referencia=dto.Referencia.Trim(); await repo.ActualizarAsync(x,ct); return true;}
 public Task<bool> EliminarAsync(int id,CancellationToken ct=default)=>repo.EliminarAsync(id,ct);
 private static DireccionDto Map(Direccion x)=>new(x.DireccionId,x.ClienteId,x.Calle,x.Numero,x.Sector,x.Ciudad,x.Referencia);
}
