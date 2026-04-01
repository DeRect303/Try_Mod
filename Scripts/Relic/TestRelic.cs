using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace Try.Scripts.Relic;

// 加入哪个遗物池，此处为通用
[Pool(typeof(SharedRelicPool))]
public class TestRelic : CustomRelicModel
{
    // 稀有度
    public override RelicRarity Rarity => RelicRarity.Common;

    // 遗物的数值。替换本地化中的{Cards}。
  //  protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    // 这个属性会被保存。建议添加前缀id防止撞车。
    // 设置不同的SerializationCondition来控制属性的保存条件，例如这里使用默认值AlwaysSave表示无论属性值是什么都保存。
    
    [SavedProperty]
    public int Test_GameTurns { get; set; } = 0;

    // 添加新的动态变量
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1), new DynamicVar("GameTurns", Test_GameTurns)];
    // 小图标
    public override string PackedIconPath => $"res://try/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    // 轮廓图标
    protected override string PackedIconOutlinePath => $"res://try/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    // 大图标
    protected override string BigIconPath => $"res://try/images/relics/{Id.Entry.ToLowerInvariant()}.png";

    
    
    
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        // 这里的DynamicVars.Cards.IntValue为上面设置的CardsVar的数值。
     //   await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, player);
     // 每回合开始时，修改Test_GameTurns的值，并改变卡牌描述中{GameTurns}的值为Test_GameTurns的值
     Test_GameTurns++;
     DynamicVars["GameTurns"].BaseValue = Test_GameTurns;
     await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, player);

    }
}