import "./../styles/advert-details-styles.css";
import { AdvancedDetailsTile } from "./AdvancedDetailsTile";

export const AdvancedDetails = () => {
  const advancedDataArray = [
    { name: "Firma:", value: "Grupa TVN" },
    { name: "Praca skupiona na:", value: "Produkcie" },
    { name: "Poziom doświadczenia", value: "Mid" },
    { name: "Liczba pracowników", value: "2500+" },
  ];
  return (
    <div className="advert-details__advanced">
      <span className="advert-details__advanced--row">
        <AdvancedDetailsTile details={advancedDataArray[0]} />
        <AdvancedDetailsTile details={advancedDataArray[1]} />
      </span>

      <span className="advert-details__advanced--row">
        <AdvancedDetailsTile details={advancedDataArray[2]} />
        <AdvancedDetailsTile details={advancedDataArray[3]} />
      </span>
    </div>
  );
};
