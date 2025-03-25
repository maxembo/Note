import { Card, CardHeader, Heading, CardBody, Text, CardFooter, Divider, Flex} from '@chakra-ui/react';
import moment from 'moment/moment';
import MenuNote from './MenuNote';


export default function Note({ note, onUpdate, onDelete }) {
  return (
    <Card color={'purple'} backgroundColor={'purple.100'} variant={'filled'}>
      <CardHeader>
        <Flex align={'center'} justify={'space-between'} w={'100%'}>
          <Heading flex={'1'} textAlign={'center'} size={'md'}>{note.title}</Heading>
          <MenuNote
            note={note}
            onUpdate={onUpdate}
            onDelete={onDelete}
          />
        </Flex>
      </CardHeader>
      <Divider borderColor={'purple'} />
      <CardBody>
        <Text>{note.description}</Text>
      </CardBody>
      <CardFooter alignSelf={'end'}>{moment(note.createdAt).format('DD.MM.YY h:mm:ss')}</CardFooter>
    </Card>
  );
}
