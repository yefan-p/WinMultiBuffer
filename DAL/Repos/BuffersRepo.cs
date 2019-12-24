using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Models;

namespace DAL.Repos
{
    public class BuffersRepo
    {

        /// <summary>
        /// Создает новый буффер с переданными параметрами
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>Возвращает количество измененных записей</returns>
        public int Create(BuffersModel buffer)
        {
            MultiBufferContext context = new MultiBufferContext();
            tblClipboards clipboard = new tblClipboards
            {
                idUser = buffer.User,
                intCopyKeyCode = buffer.CopyKeyCode,
                intPasteKeyCode = buffer.PasteKeyCode,
                nvcName = buffer.Name,
                nvcValue = buffer.Value,
            };
            context.tblClipboards.Add(clipboard);
            return context.SaveChanges();
        }

        /// <summary>
        /// Возвращает список всех буферов по id указанного пользователя
        /// </summary>
        /// <param name="idUser">Id пользователя, по которому необходимо вернуть все буферы</param>
        /// <returns></returns>
        public IEnumerable<BuffersModel> Read(int idUser)
        {
            MultiBufferContext context = new MultiBufferContext();

            return 
                (from el in context.tblClipboards
                 where el.idUser == idUser
                 select new BuffersModel
                 {
                     Id = el.id,
                     User = el.idUser,
                     CopyKeyCode = el.intCopyKeyCode,
                     PasteKeyCode = el.intPasteKeyCode,
                     Name = el.nvcName,
                     Value = el.nvcValue,
                 }).ToList();
        }

        /// <summary>
        /// Обновляет значение указанного буфера
        /// </summary>
        /// <param name="idBuffer">Id буфера, значение которого необходимо обновить</param>
        /// <param name="value">Новое значения для буфера</param>
        /// <returns>Возвращает количество измененных записей</returns>
        public int Update(int idBuffer, string value)
        {
            MultiBufferContext context = new MultiBufferContext();

            tblClipboards clipboard =
                (from el in context.tblClipboards
                where el.id == idBuffer
                select el).FirstOrDefault();

            if (clipboard == null)
                return -1;

            clipboard.nvcValue = value;
            return context.SaveChanges();
        }

        /// <summary>
        /// Удаляет указанный буфер
        /// </summary>
        /// <param name="idBuffer">Id буфера, который необходимо удалить</param>
        /// <returns>Возвращает количество измененных записей</returns>
        public int Delete(int idBuffer)
        {
            MultiBufferContext context = new MultiBufferContext();

            tblClipboards clipboard =
                (from el in context.tblClipboards
                 where el.id == idBuffer
                 select el).FirstOrDefault();

            if (clipboard == null)
                return -1;

            context.tblClipboards.Remove(clipboard);
            return context.SaveChanges();
        }
    }
}
