using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.Shared.Components;

[Component(typeof(IconDetail))]
[View(@"
<Container Style=""TextureFont: Default; Width: 500; LeftOffset: 300; Padding: 10, 10, 10, 10; NinePatch: Sample_NinePatch; ColumnGap: 5"">
  <Container>
    <Container>Overview</Container>
    <Container Style=""TextAlign: Left"">
      The main goal is to manage the city's growth and to get the most population on the tiles you own while, adding negative effects to your opponents to decrease their population amount.
    </Container>
  </Container>
  <Container>
    <Container>Turn Details</Container>
    <Container Style=""ColumnGap: 5"">
      <Container Style=""TextAlign: Left"">. Play any number of notices on the grid</Container>
      <Container Style=""TextAlign: Left"">. Place up to 2 zones on the board, unless increased by notice</Container>
      <Container Style=""TextAlign: Left"">. Place a card onto the board on the appropriate zoned tile, this ends the turn</Container>
    </Container>
  </Container>
  <Container>
    <Container>Icons</Container>
    <Container Style=""ColumnGap: 15"">
      <IconDetail :Frame=""0"">
        Population accounts for your score, the higher the population the better your score is.
      </IconDetail>

      <IconDetail :Frame=""5"">
        Population Cap determines the maximum population The specific tile can support.
      </IconDetail>

      <IconDetail :Frame=""1"">
        Crime represents the level of criminal activity in the tile, and will decrease your population on that tile.
      </IconDetail>

      <IconDetail :Frame=""2"">
        Pollution represents the level of environmental damage in the tile, and will decrease the population cap of that tile.
      </IconDetail>

      <IconDetail :Frame=""3"">
        Virtue represents the level of moral and ethical standards in the tile, and will negate all crime on that tile.
      </IconDetail>
      
      <IconDetail :Frame=""4"">
        Conservation represents the level of environmental protection in the tile, and will negate all pollution on that tile.
      </IconDetail>
    </Container>
  </Container>
</Container>
")]
internal class InfoComponent : UIComponent
{
  public readonly ReactiveProperty<bool> ShowDetails;

  public InfoComponent()
  {
    ShowDetails = new ReactiveProperty<bool>(false);
  }
}
