using Backend.Models;

namespace Backend.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<RoleModel> GetRoles();

        RoleModel GetRole(int id);
        RoleModel GetRole(string name);
        bool RoleExists(int id);
        bool RoleExists(string name);
       
        bool CreateRole(RoleModel role);
        bool UpdateRole(RoleModel role);
        bool DeleteRole(RoleModel role);
        bool Save();

    }
}
