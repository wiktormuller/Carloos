import '../styles/data-styles.css';

export const AdvertSynopsis = (props) => {

  const findNameById=(id, array)=>{
    let name;
    array.forEach((x)=>{
      if(x.id === id){
        name = x.name;
      }
    })
    return name
  }

  const renderArray = props.skills.map(
    (skill) => {
      return <div key={skill} className="data__advert-data--synopsis--skill">{findNameById(skill, props.skillsArray)}</div>
    }
  )

  return (
    <div className="data__advert-data--synopsis">
      <div className="data__advert-data--synopsis--header"><b>{props.title}</b><span><b>Data dodania: </b>{props.date}</span></div>
      <div className="data__advert-data--synopsis--body">
        <span><b>Lokalizacja: </b>{findNameById(props.localization, props.localizationArray)   }</span>
        <span><b>Pensja: </b>{props.salary}</span>
        <span><b>Opis: </b>{props.description}</span>
      </div>
      <div className="data__advert-data--synopsis--skills">
          {renderArray}
      </div>
    </div>
  );
  };