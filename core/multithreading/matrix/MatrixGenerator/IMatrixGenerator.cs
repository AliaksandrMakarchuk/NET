using matrix.UserInterface;

namespace matrix.MatrixGenerator
{
    public interface IMatrixGenerator
    {
        void Generate(int row, int columns, IUserInterface userInterface);
    }
}