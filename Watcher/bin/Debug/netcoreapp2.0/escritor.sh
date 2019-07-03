#!/bin/bash
brasil="Jun 16 15:33:05 portal.inf.ufsm.br sshd[27156]: Accepted publickey for rtrindade from 179.217.246.223 port 48720 ssh2: RSA SHA256:X15PDVHhl8jR5EGDjegvnFZNyAtzVmQKcjD68k6H72A"
china="Jun 16 22:21:27 portal.inf.ufsm.br sshd[31991]: Accepted keyboard-interactive/pam for lferreira from 218.107.132.66 port 36816 ssh2"
uk="Jun 17 00:45:19 portal.inf.ufsm.br sshd[33080]: Accepted publickey for jvlima from 213.228.229.96 port 53968 ssh2: RSA SHA256:JIWkccfDjVZJn4YoDObSDYfU3rmL/kGnn53D/Sy6Y/k"
franca="Jun 17 00:45:19 portal.inf.ufsm.br sshd[33080]: Accepted publickey for jfgez from 176.31.84.249 port 53968 ssh2: RSA SHA256:JIWkccfDjVZJn4YoDObSDYfU3rmL/kGnn53D/Sy6Y/k"
for i in {1..3}; do echo $brasil >> log.txt; done
for i in {1..4}; do echo $china >> log.txt; done
for i in {1..10}; do echo $brasil >> log.txt; done
for i in {1..2}; do echo $uk >> log.txt; done
for i in {1..8}; do echo $brasil >> log.txt; done
for i in {1..1}; do echo $franca >> log.txt; done
