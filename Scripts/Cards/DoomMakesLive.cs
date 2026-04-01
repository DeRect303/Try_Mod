using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Try.Scripts.Powers;

namespace Try.Scripts.Cards;


[Pool(typeof(NecrobinderCardPool))]
public class DoomMakesLive : CustomCardModel
{
    private const int energycost = 3;

    private const CardType type = CardType.Power;
    
    private const CardRarity rarity = CardRarity.Rare;
    
    private const TargetType target = TargetType.Self;
    
    private const bool shouldShowInCardLibrary = true;


    public DoomMakesLive() : base(energycost, type, rarity, target ,shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        DoomMakesLivePower doomMakesLive = await PowerCmd.Apply<DoomMakesLivePower>(this.Owner.Creature,1,this.Owner.Creature ,(CardModel) this);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}