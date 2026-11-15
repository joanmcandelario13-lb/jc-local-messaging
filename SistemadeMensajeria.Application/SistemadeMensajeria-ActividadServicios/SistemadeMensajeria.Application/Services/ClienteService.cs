using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Application.Contract;
using SistemadeMensajeria.Application.Dtos;
using SistemadeMensajeria.Application.Core;
namespace SistemadeMensajeria.Application.Services;
public class ClienteService(IRepositorio<Cliente> repo, IUnitOfWork unitOfWork) : BaseService, IClienteService {
 public async Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(CancellationToken ct=default)=>(await repo.ObtenerTodosAsync(ct)).Select(Map).ToList();
 public async Task<ClienteDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); return x is null?null:Map(x);}
 public async Task<ClienteDto> CrearAsync(ClienteRequestDto dto,CancellationToken ct=default) {  var x=await repo.AgregarAsync(new Cliente { Nombres=dto.Nombres.Trim(), Apellidos=dto.Apellidos.Trim(), Telefono=dto.Telefono.Trim(), Correo=dto.Correo.Trim() },ct); await unitOfWork.SaveChangesAsync(ct); return Map(x); }
 public async Task<bool> ActualizarAsync(int id,ClienteRequestDto dto,CancellationToken ct=default) {var x=await repo.ObtenerPorIdAsync(id,ct); if(x is null)return false;  x.Nombres=dto.Nombres.Trim(); x.Apellidos=dto.Apellidos.Trim(); x.Telefono=dto.Telefono.Trim(); x.Correo=dto.Correo.Trim(); await repo.ActualizarAsync(x,ct); await unitOfWork.SaveChangesAsync(ct); return true;}
 public async Task<bool> EliminarAsync(int id,CancellationToken ct=default) { var eliminado=await repo.EliminarAsync(id,ct); if(eliminado) await unitOfWork.SaveChangesAsync(ct); return eliminado; }
 private static ClienteDto Map(Cliente x)=>new(x.ClienteId,x.Nombres,x.Apellidos,x.Telefono,x.Correo,x.FechaRegistro);
}
