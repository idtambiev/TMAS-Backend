using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.Models.Enums
{
        public enum UserActions:byte
        {
        CreateCard = 0,
        DeleteCard = 1,
        UpdateCard = 2,
        MoveCard = 3,
        MoveCardOnOtherColumn = 4,
        CreateColumn = 5,
        DeleteColumn = 6,
        UpdateColumn = 7,
        MoveColumn = 8,
        CheckedCard=9,
        UncheckedCard=10,
        AddedDescription = 11,
        EditedDescription = 12,
        ChangeExecutionPeriod = 13,
        AssignUser=14,
        UnassignUser=15
    }
}
