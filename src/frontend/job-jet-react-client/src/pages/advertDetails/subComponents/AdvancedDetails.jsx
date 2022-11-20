import "./../styles/advert-details-styles.css";
import { AdvancedDetailsTile } from "./AdvancedDetailsTile";

export default function AdvancedDetails(props)
{
  return (
    <div className="advert-details__advanced">
      <span className="advert-details__advanced--row">
        <AdvancedDetailsTile details={props.advancedDataArray[0]} />
        <AdvancedDetailsTile details={props.advancedDataArray[1]} />
      </span>

      <span className="advert-details__advanced--row">
        <AdvancedDetailsTile details={props.advancedDataArray[2]} />
        <AdvancedDetailsTile details={props.advancedDataArray[3]} />
      </span>
    </div>
  );
}