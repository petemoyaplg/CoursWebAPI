﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Northwin2.Exceptions
{
    public static class DbUpdateExceptionExtensions
    {
        private const int NotNullError = 515;
        private const int IdentityError = 544;
        private const int ForeignKeyError = 547;
        private const int UniqueError = 2601;
        private const int PrimaryKeyError = 2627;
        private const int MaxLengthError = 2628;
        private const int NumericValueError = 8115;

        // Traduit une DbUpdateException en un objet de type ProblemDetails
        // utilisable pour construire une réponse HTTP
        public static ProblemDetails ConvertToProblemDetails(this DbUpdateException ex)
        {
            if (ex.InnerException is not SqlException sqlEx)
                throw new NotImplementedException("Traduction des erreurs en réponses HTTP non implémentée pour ce SGBD");

            (HttpStatusCode StatusCode, string Message) err;
            switch (sqlEx.Number)
            {
                case NotNullError:
                    err = (HttpStatusCode.BadRequest, "Impossible d'affecter la valeur Null à un champ non nullable.");
                    break;

                case IdentityError:
                    err = (HttpStatusCode.Conflict, "Impossible d'affecter une valeur à un identifiant auto-incrémenté.");
                    break;

                case ForeignKeyError:
                    err = (HttpStatusCode.BadRequest, "La requête fait référence à un enregistrement qui n'existe pas dans la base\n" +
                           " ou bien tente de supprimer un enregistrement référencé ailleurs dans la base");
                    break;

                case UniqueError:
                case PrimaryKeyError:
                    err = (HttpStatusCode.Conflict, "Un enregistrement de même identifiant existe déjà dans la base.");
                    break;

                case NumericValueError:
                    err = (HttpStatusCode.BadRequest, "Une valeur numérique incorrecte a été fournie.");
                    break;

                case MaxLengthError:
                    err = (HttpStatusCode.BadRequest, "Une chaîne trop longue a été fournie.");
                    break;

                default:
                    err = (HttpStatusCode.InternalServerError, "Erreur non gérée à l'enregistrement dans la base de données.");
                    break;
            }

            return new ProblemDetails
            {
                Title = err.StatusCode.ToString(),
                Status = (int)err.StatusCode,
                Detail = err.Message
            };
        }
    }
}
