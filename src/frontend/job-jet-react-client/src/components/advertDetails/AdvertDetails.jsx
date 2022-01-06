import "./styles/advert-details-styles.css";
import { BasicDetails } from "./subComponents/BasicDetails";
import { AdvancedDetails } from "./subComponents/AdvancedDetails";

export const AdvertDetails = () => {
  return (
    <div className="advert-details">
      <BasicDetails />
      <AdvancedDetails />
    </div>
  );
};
