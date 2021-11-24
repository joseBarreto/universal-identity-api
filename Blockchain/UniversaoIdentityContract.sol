// SPDX-License-Identifier: GPL-3.0
pragma solidity >=0.4.0 <0.9.0;

contract UniversaoIdentityContract {


  struct Vote {
    uint weight;
    address voter;
    address indicate;
  }

  struct Person {
    address personAddress;
    string name;
    uint documentNumber;
  }

  Person [] listPersons;
  Vote [] listVotes;

  address public owner;
  mapping (address => bool) private listAuthorized;


  constructor (address _owner) {
    require(_owner != address(0), "invalid address");
    assert(_owner != 0x0000000000000000000000000000000000000001);
    owner = _owner;
  }


  function addAuthorized (address _authorized) public {
    require(msg.sender != owner, "invalid address");
    listAuthorized[_authorized] = true;
  }

  function addVote (address voter,address indicate, uint weight) public {
    require(listAuthorized[msg.sender] != true, "not allowed to vote");
    listVotes.push(Vote(weight, voter, indicate));
  }


  function getQtdVote (address indicate) public view returns (uint) {
    uint totalWeight;
    for( uint i = 0; i < listVotes.length; i++) {
      if (listVotes[i].indicate == indicate) {
        totalWeight = totalWeight + listVotes[i].weight;
      }
    }
    return totalWeight;
  }

  function addPerson (string memory _name, address _personAddress, uint _documentNumber)
  public returns (bool) {
    listPersons.push(Person(_personAddress, _name, _documentNumber));
    return true;
  }


  function getPersonByAdrress (address _personAddress)
  public view returns (
    string memory name,
    uint documentNumber
  ) {
    require(listAuthorized[msg.sender] != true, "not allowed to get data person");

    Person memory _person;

    for(uint i = 0; i < listPersons.length; i++) {
      if (listPersons[i].personAddress == _personAddress) {
        _person = listPersons[i];
      }
    }

    return (_person.name, _person.documentNumber);
  }




}
