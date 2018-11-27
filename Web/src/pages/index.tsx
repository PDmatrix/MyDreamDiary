import React from "react";
import { Segment } from "../components/Shared/Segment";
import { Row, Col } from "antd";
import styles from "./index.css";

const Index: React.FunctionComponent = () => {
  return (
    <Row className={styles.bgimg}>
      <Col span={12}>
        <Segment>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut tempus
            feugiat felis, eget sollicitudin metus convallis vitae. Nullam eget
            sollicitudin tellus. Maecenas euismod, quam vitae rhoncus
            pellentesque, risus nisi accumsan ipsum, id gravida tortor massa sed
            enim. Suspendisse potenti. Aliquam elementum consequat orci, non
            egestas mauris pulvinar vitae. In a consequat ex, eu aliquam libero.
            Nam dapibus orci sit amet ligula eleifend, vel suscipit metus
            rhoncus. Sed vehicula vel lacus sit amet rutrum. Fusce ultricies
            arcu sit amet fringilla bibendum. Suspendisse porttitor sit amet
            mauris nec imperdiet. Vestibulum convallis quam id risus condimentum
            tristique eget ac risus. Curabitur iaculis nulla vitae dui cursus
            condimentum quis ut turpis. Etiam feugiat metus sed consectetur
            vehicula. Sed ultricies justo et ante mollis, auctor placerat diam
            tincidunt. Interdum et malesuada fames ac ante ipsum primis in
            faucibus.
          </p>
        </Segment>
      </Col>
    </Row>
  );
};

export default Index;
