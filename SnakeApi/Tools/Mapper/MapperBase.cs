using SnakeApi.Interfaces;
using System.Reflection;

namespace SnakeApi.Tools.Mapper
{
    public class MapperBase<Input, Output> : IMapperBase<Input, Output>
        where Input : new()
        where Output : new()
    {
        private void TransferProperty(PropertyInfo property, object fromValue, object whereToValue)
        {
            try
            {
                string propName = property.Name;
                var propValue = property.GetValue(fromValue);
                //las propiedades de ambas clases deben tener el mismo nombre
                // y mismo tipo
                var type = whereToValue.GetType();
                var prop = type.GetProperty(propName);
                if (prop == null) { throw new InvalidOperationException($"Could not transfer property {propName}"); }
                prop.SetValue(whereToValue, propValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Output? Set(Input? dto)
        {
            if (dto == null) { return default; }
            Output entity = new Output();
            PropertyInfo[] properties = typeof(Input).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                TransferProperty(property, dto, entity);
            }
            return entity;
        }

        public virtual Input? Get(Output? entity)
        {
            if (entity == null) { return default; }
            Input dto = new Input();
            PropertyInfo[] properties = typeof(Output).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                TransferProperty(property, entity, dto);
            }
            return dto;
        }

        public virtual List<Output>? Set(List<Input>? dtoList)
        {
            if (dtoList == null || dtoList.Count == 0) { return default; }
            List<Output> lst = new List<Output>();
            foreach (Input dto in dtoList)
            {
                var item = Set(dto);
                if (item == null) { continue; }
                lst.Add(item);
            }
            return lst;
        }

        public virtual List<Input>? Get(List<Output>? entityList)
        {
            if (entityList == null || entityList.Count == 0) { return default; }
            List<Input> dtoList = new List<Input>();
            foreach (Output entity in entityList)
            {
                var item = Get(entity);
                if (item == null) { continue; }
                dtoList.Add(item);
            }
            return dtoList;
        }
    }
}