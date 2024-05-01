using Backend.Data;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly DataContext _context;
        public UserInfoRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUserInfo(UserInfoModel userinfo)
        {
            _context.Add(userinfo);
            return Save();
        }

        public bool DeleteUserInfo(UserInfoModel userinfo)
        {
            _context.Remove(userinfo);
            return Save();
        }

        public ICollection<UserInfoModel> GetUserInfos()
        {
            return _context.UserInfo.OrderBy(p => p.Id).ToList();
        }

        public UserInfoModel GetUserInfo(int id)
        {
            return _context.UserInfo.Where(p => p.Id == id).FirstOrDefault();
        }

        public UserInfoModel GetUserInfo(string firstname)
        {
            return _context.UserInfo.Where(p => p.FirstName == firstname).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserInfo(UserInfoModel userinfo)
        {
            _context.Update(userinfo);
            return Save();
        }
    }
}
