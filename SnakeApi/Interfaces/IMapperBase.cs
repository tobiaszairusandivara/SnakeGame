namespace SnakeApi.Interfaces
{
    public interface IMapperBase <Input, Output>
    {
        Output? Set(Input? dto);
        Input? Get(Output? entity);
        List<Output>? Set(List<Input>? dtoList);
        List<Input>? Get(List<Output>? entityList);
    }
}
