import '../styles/data-styles.css';
import { AdvertSynopsis } from "./AdvertSynopsis"

export const AdvertData = () => {

    const adsArray = [
      {id:1, title:"Praca Trójmiasto", localization:"Trójmiasto", salary:"5000-7500", date:"20-10-2021", description:"Poszukiwany Junior Web Developer do pracy na pół etatu"}
    ];

    const renderedArray = adsArray.map(
      (ad) => {
        return <AdvertSynopsis 
            key={ad.id} 
            title={ad.title}
            localization={ad.localization}
            salary={ad.salary}
            date={ad.date}
            description={ad.description}
            >
            </AdvertSynopsis>
      }
    )

    return (
      <div className="data__advert-data">
        {renderedArray}
      </div>
      );
}