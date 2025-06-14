using Discord;
using Discord.Commands;
using PKHeX.Core;
using System.Threading.Tasks;

namespace SysBot.Pokemon.Discord;

[Summary("Queues new Random/Ledy trades")]
public class RandomSelectModule<T> : ModuleBase<SocketCommandContext> where T : PKM, new()
{
    private static TradeQueueInfo<T> Info => SysCord<T>.Runner.Hub.Queues.Info;

    [Command("random")]
    [Alias("r", "event")]
    [Summary("Allows a manual Ledy trade")]
    [RequireQueueRole(nameof(DiscordManager.RolesClone))]
    public Task RandomAsync(int code)
    {
        var sig = Context.User.GetFavor();
        return QueueHelper<T>.AddToQueueAsync(Context, code, Context.User.Username, sig, new T(), PokeRoutineType.LinkTrade, PokeTradeType.Random);
    }

    [Command("random")]
    [Alias("r", "event")]
    [Summary("Allows a manual Ledy trade")]
    [RequireQueueRole(nameof(DiscordManager.RolesClone))]
    public Task RandomAsync([Summary("Trade Code")][Remainder] string code)
    {
        int tradeCode = Util.ToInt32(code);
        var sig = Context.User.GetFavor();
        return QueueHelper<T>.AddToQueueAsync(Context, tradeCode == 0 ? Info.GetRandomTradeCode() : tradeCode, Context.User.Username, sig, new T(), PokeRoutineType.LinkTrade, PokeTradeType.Random);
    }

    [Command("random")]
    [Alias("r")]
    [Summary("Allows a manual Ledy trade")]
    [RequireQueueRole(nameof(DiscordManager.RolesClone))]
    public Task RandomAsync()
    {
        var code = Info.GetRandomTradeCode();
        return RandomAsync(code);
    }
}
