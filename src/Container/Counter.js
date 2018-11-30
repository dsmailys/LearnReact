import { connect } from 'react-redux';
import { INCREMENT, DECREMENT } from '../Reducers/counterTypes';
import Counter from '../Components/Counter';

const mapDispatchToProps = dispatch => {
  return {
    onIncrement: () => dispatch({ type: INCREMENT }), 
    onDecrement: () => dispatch({ type: DECREMENT })
  };
};

const mapStateToProps = state => {
  return {
    value: state.counter
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Counter);