
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Security;

namespace crm
{
    public static partial class Games
    {
        public static Task Start(IOwinContext ctx)
        {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            var args = ctx.Parse();


            if (database.Games.Start(args))
            {

                JObject response = new JObject();

                response["seconds"] = crm.RouletBackground.Instance.GetSeconds();

                response["time"] = Validator.CurentUnixTime();

                return ctx.JSON(response);

            }
            else
            {
                return ctx.Error(400);
            }

        }

        public static Task End(IOwinContext ctx)
        {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            var args = ctx.Parse();

            if (Validator.GetGuid(args["user_id"]) == null)
            {
                return ctx.Error(401);
            }

            if (database.Games.End(args))
            {
                return ctx.OK();
            }
            else
            {
                return ctx.Error(400);
            }

        }

        public static Task Find(IOwinContext ctx) {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            var args = ctx.Parse();

            Guid? id = Validator.GetGuid(args["id"]);

            JObject game = database.Games.Find(id);

            return ctx.JSON(game);

        }

        public static Task FindInfo(IOwinContext ctx)
        {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            var args = ctx.Parse();

            Guid? id = Validator.GetGuid(args["id"]);

            JArray game = database.Games.FindGame(id);

            JObject response = new JObject();

            response["data"] = game;

            response["seconds"] = crm.RouletBackground.Instance.GetSeconds();

            response["time"] = Validator.CurentUnixTime();

            return ctx.JSON(response);

        }

        public static Task Update(IOwinContext ctx)
        {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            var args = ctx.Parse();

            if (args["must_bet"] == null)
            {
                return ctx.Error(401);
            }

            if (database.Games.Update(args))
            {
                return ctx.OK();
            }
            else
            {
                return ctx.Error(400);
            }

        }

        public static Task Bet(IOwinContext ctx)
        {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            var args = ctx.Parse();

            if (Validator.GetGuid(args["game_id"]) == null)
            {
                return ctx.Error(401);
            }

            if (Validator.GetGuid(args["user_id"]) == null)
            {
                return ctx.Error(401);
            }


            if (args["coin"] == null)
            {
                return ctx.Error(401);
            }

          
            if (database.Games.Bet(args))
            {
                return ctx.OK();
            }
            else
            {
                return ctx.Error(400);
            }

        }

        public static Task List(IOwinContext ctx)
        {

            if (!Authentication.Check(ctx))
            {
                return ctx.Error(403);
            }

            JArray games = database.Games.List();

            return ctx.JSON(games);

        }
    }
}