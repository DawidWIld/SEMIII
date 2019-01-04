<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:f="http://example.com/functions">
  <xsl:output method="text"/>
  <xsl:template match="/root">
    <xsl:apply-templates select="stats"/>
    <xsl:apply-templates select="incidents"/>
  </xsl:template>
  <xsl:template match="incidents">
    <xsl:apply-templates select="incident"/>
  </xsl:template>
  <xsl:template match="stats">
*******************************************************************************
*                               STATISTICS:                                   *
* Number of incidents: <xsl:value-of select="numberOfIncidents" />*
* Most common category: <xsl:value-of select="mostCommonCategory" />*
* Report generation date: <xsl:value-of select="reportDate" />*
*******************************************************************************
  </xsl:template>
  <xsl:template match="incident">
+======================+===========================+==========================+
| ID: <xsl:value-of select="substring(concat(incidentID, '                              '), 1, 16)" /> | User: <xsl:value-of select="endUser" /> | Date: <xsl:value-of select="dateOpened" /> |
+======================+===========================+==========================+
| State: <xsl:value-of select="state" /> | Category: <xsl:value-of select="category/mainCategory" />/<xsl:value-of select="category/subcategory" /> |
*================+=====+=========================+============================+
| Priority: #### | Group: <xsl:value-of select="assignmentGroup" /> | Phone: <xsl:value-of select="phoneNumber" />(PL) |
*======================+=========================+============================+
| Short: <xsl:value-of select="shortDescription" /> |
*=============================================================================+
|                              Description:                                   |
| <xsl:value-of select="description" /> |
+=============================================================================+
| Notes: <xsl:value-of select="workNotes" /> |
*=============================================================================+
    
  </xsl:template>
</xsl:stylesheet>
