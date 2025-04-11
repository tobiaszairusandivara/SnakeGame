using SnakeApi.Models;
using SnakeApi.Models.DTOs;

namespace SnakeApi.Tools.Mapper
{
    public class ScoreMapper : MapperBase<ScoreDTO, Score>
    {
        public override Score? Set(ScoreDTO dto)
        {
            return base.Set(dto);
        }

        public override ScoreDTO? Get(Score entity)
        {
            return base.Get(entity);
        }

        public override List<Score> Set(List<ScoreDTO> dtoList)
        {
            return base.Set(dtoList);
        }

        public override List<ScoreDTO> Get(List<Score> entityList)
        {
            return base.Get(entityList);
        }
    }
}
