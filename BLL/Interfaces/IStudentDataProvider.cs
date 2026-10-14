using BLL.Entities;

namespace BLL.Interfaces;

public interface IStudentDataProvider
{
    List<Student> GetAll();
    void SaveAll(List<Student> students);
}