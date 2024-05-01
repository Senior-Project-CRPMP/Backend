using Backend.Data;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateRole(RoleModel role)
        {
            _context.Add(role);
            return Save();
        }

        public bool DeleteRole(RoleModel role)
        {
            _context.Remove(role);
            return Save();
        }

        public RoleModel GetRole(int id)
        {
            return _context.Roles.Where(p => p.Id == id).FirstOrDefault();
        }

        public RoleModel GetRole(string name)
        {
            return _context.Roles.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<RoleModel> GetRoles()
        {
            return _context.Roles.OrderBy(p => p.Id).ToList();
        }

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(p => p.Id == id);
        }

        public bool RoleExists(string name)
        {
            return _context.Roles.Any(p => p.Name == name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRole(RoleModel role)
        {
            _context.Update(role);
            return Save();
        }
    }
}
