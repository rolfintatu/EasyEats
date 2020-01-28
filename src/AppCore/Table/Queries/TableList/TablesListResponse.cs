using AppCore.Dtos;
using AppCore.Entities;
using AppCore.Enums;
using AppCore.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Table.Queries.TableList
{
    public class TablesListResponse
    {
        public List<TableDetailsDto> Tables { get; set; }
    }

}
