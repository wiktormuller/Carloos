import "./styles/advert-details-styles.css";
import { BasicDetails } from "./subComponents/BasicDetails";
import { AdvancedDetails } from "./subComponents/AdvancedDetails";
import { Description } from "./subComponents/Description";

export const AdvertDetails = (props) => {
  return (
    <div className="advert-details">
      <BasicDetails
        title={props.advertDetails.title}
        address={props.advertDetails.address}
        conditions={props.advertDetails.conditions}
      />
      <AdvancedDetails
        advancedDataArray={props.advertDetails.advancedDataArray}
      />
      <Description description={props.advertDetails.description} />
    </div>
  );
};
