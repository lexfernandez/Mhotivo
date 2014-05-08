using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Mhotivo.App_Data;
using Mhotivo.App_Data.Repositories;
using System.Data.Entity;
using System.Globalization;

namespace Mhotivo.Models
{
    public class DiaryEvent
    {
        public int DiaryEventId;
        public String Title;
        public int SomeImportantKeyId;
        public String StartDateString;
        public String EndDateString;
        public String StatusString;
        public String StatusColor;
        public String ClassName;
    }
}