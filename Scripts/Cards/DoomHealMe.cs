using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Try.Scripts.Cards;


[Pool(typeof(NecrobinderCardPool))]
public class DoomHealMe : CustomCardModel
{
    private const int energycost = 1;

    private const CardType type = CardType.Skill;
    
    private const CardRarity rarity = CardRarity.Common;

    private const TargetType target = TargetType.Self;
    
    private const bool shouldShowInCardLibrary = true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<DoomPower>(12),
        new HealVar(6)
    ];
    
    public override string PortraitPath => $"res://try/images/cards/{Id.Entry.ToLowerInvariant()}.png";

    
    
    public DoomHealMe() : base(energycost, type, rarity, target ,shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
       DoomPower doomPower=  await PowerCmd.Apply<DoomPower>(this.Owner.Creature,this.DynamicVars.Doom.BaseValue,this.Owner.Creature ,(CardModel) this);
       await CreatureCmd.Heal(this.Owner.Creature ,this.DynamicVars.Heal.BaseValue);
    }
    protected override void OnUpgrade() => this.DynamicVars.Heal.UpgradeValueBy(2);
}