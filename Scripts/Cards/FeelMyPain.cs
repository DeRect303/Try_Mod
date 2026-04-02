using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Try.Scripts.Cards;


[Pool(typeof(NecrobinderCardPool))]
public class FeelMyPain : CustomCardModel
{
    private const int energycost = 1;

    private const CardType type = CardType.Attack;
    
    private const CardRarity rarity = CardRarity.Uncommon;

    private const TargetType target = TargetType.AnyEnemy;

    private const bool shouldShowInCardLibrary = true;
    
    public FeelMyPain() : base(energycost, type, rarity, target ,shouldShowInCardLibrary)
    {
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<DoomPower>(8)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.IsUpgraded)
        {
            DoomPower doomPower = await PowerCmd.Apply<DoomPower>(this.Owner.Creature, this.DynamicVars.Doom.BaseValue,this.Owner.Creature,(CardModel) this) ;
        }
        if (this.Owner.Creature.GetPower<DoomPower>() != null)
        {
            var amount = this.Owner.Creature.GetPower<DoomPower>().Amount;
            await DamageCmd.Attack(amount)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .Execute(choiceContext);
            DoomPower doomPower = await PowerCmd.Apply<DoomPower>(this.Owner.Creature, amount,this.Owner.Creature,(CardModel) this) ;
        }
    }

}