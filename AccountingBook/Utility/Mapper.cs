using AccountingBook.Models.DTO;
using AccountingBook.Repositories.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Utility
{
    internal class Mapper
    {
        public static IEnumerable<T> Map<TSource, T>(IEnumerable<TSource> source, Action<IMappingExpression<TSource, T>> action = null)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                var map = cfg.CreateMap<TSource, T>();
                if (action != null)
                    action(map);
            }).CreateMapper();

            return mapper.Map<IEnumerable<T>>(source);
        }

        public static T Map<TSource, T>(TSource source, Action<IMappingExpression<TSource, T>> action = null)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                var map = cfg.CreateMap<TSource, T>();
                if (action != null)
                    action(map);
            }).CreateMapper();

            return mapper.Map<T>(source);


        }
    }
}

