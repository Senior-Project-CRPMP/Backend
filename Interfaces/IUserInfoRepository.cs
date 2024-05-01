using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserInfoRepository
    {
        ICollection<UserInfoModel> GetUserInfos();

        UserInfoModel GetUserInfo(int id);
        UserInfoModel GetUserInfo(string firstname);

        bool CreateUserInfo(UserInfoModel userinfo);
        bool UpdateUserInfo(UserInfoModel userinfo);
        bool DeleteUserInfo(UserInfoModel userinfo);
        bool Save();

    }
}
