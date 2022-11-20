import "../styles/advert-details-styles.css";

export default function Description(props)
{
  return (
    <div className="advert-details__description">
      {props.description}
    </div>
  );
}