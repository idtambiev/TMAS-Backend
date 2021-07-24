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
        UncheckedCard=10
        //AddCard=0,
        //EditCard=1,
        //DeleteCard=2,
        //AddColumn=3,
        //EditColumn=4,
        //DeleteColumn=5,
        //CheckCardDone=6,
        //CheckCardUndone=7,
        //MoveCard=8,
        //AddBoard=9,
        //DeleteBoard=10,
        //EditBoard=11
    }
}
