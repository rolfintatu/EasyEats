using Application.Common.Dtos;
using Domain.Enums;
using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Table.Queries.TableList
{
    public class TablesListResponse
    {
        public List<TableDetailsDto> Tables { get; set; }
    }

}
