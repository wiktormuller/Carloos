import '../styles/data-styles.css';

export const AdvertSynopsis = (props) => {
    return (
      <div className="data__advert-data--synopsis">
        <div className="data__advert-data--synopsis--header"><b>{props.title}</b><span>{`Data dodania: ${props.date}`}</span></div>
        <div className="data__advert-data--synopsis--body">
          <span>{`Lokalizacja: ${props.localization}`}</span>
          <span>{`Pensja: ${props.salary}`}</span>
          <span>{`Opis: ${props.description}`}</span>
        </div>
      </div>
    );
  };