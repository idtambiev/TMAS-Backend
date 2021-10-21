using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface ICardsSortService
    {
        Task MoveOnNewColumn(CardViewDTO card);
        Task MoveOnOldColumn(Card card);
        Task SwitchCards(int prevPosition, CardViewDTO card);
        Task<Card> ReduceAfterDeleteAsync(int id);
    }
}
